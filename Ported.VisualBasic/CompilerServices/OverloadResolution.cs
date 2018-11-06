// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.OverloadResolution
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ported.VisualBasic.CompilerServices
{
  internal class OverloadResolution
  {
    private OverloadResolution()
    {
    }

    private static bool IsExactSignatureMatch(ParameterInfo[] LeftSignature, int LeftTypeParameterCount, ParameterInfo[] RightSignature, int RightTypeParameterCount)
    {
      ParameterInfo[] parameterInfoArray1;
      ParameterInfo[] parameterInfoArray2;
      if (LeftSignature.Length >= RightSignature.Length)
      {
        parameterInfoArray1 = LeftSignature;
        parameterInfoArray2 = RightSignature;
      }
      else
      {
        parameterInfoArray1 = RightSignature;
        parameterInfoArray2 = LeftSignature;
      }
      int length = parameterInfoArray2.Length;
      int num1 = checked (parameterInfoArray1.Length - 1);
      int index1 = length;
      while (index1 <= num1)
      {
        if (!parameterInfoArray1[index1].IsOptional)
          return false;
        checked { ++index1; }
      }
      int num2 = 0;
      int num3 = checked (parameterInfoArray2.Length - 1);
      int index2 = num2;
      while (index2 <= num3)
      {
        Type type1 = parameterInfoArray2[index2].ParameterType;
        Type type2 = parameterInfoArray1[index2].ParameterType;
        if (type1.IsByRef)
          type1 = type1.GetElementType();
        if (type2.IsByRef)
          type2 = type2.GetElementType();
        if (type1 != type2 && (!parameterInfoArray2[index2].IsOptional || !parameterInfoArray1[index2].IsOptional))
          return false;
        checked { ++index2; }
      }
      return true;
    }

    private static void CompareNumericTypeSpecificity(Type LeftType, Type RightType, ref bool LeftWins, ref bool RightWins)
    {
      if (LeftType == RightType)
        return;
      if (ConversionResolution.NumericSpecificityRank[(int) Symbols.GetTypeCode(LeftType)] < ConversionResolution.NumericSpecificityRank[(int) Symbols.GetTypeCode(RightType)])
        LeftWins = true;
      else
        RightWins = true;
    }

    private static void CompareParameterSpecificity(Type ArgumentType, ParameterInfo LeftParameter, MethodBase LeftProcedure, bool ExpandLeftParamArray, ParameterInfo RightParameter, MethodBase RightProcedure, bool ExpandRightParamArray, ref bool LeftWins, ref bool RightWins, ref bool BothLose)
    {
      BothLose = false;
      Type type1 = LeftParameter.ParameterType;
      Type type2 = RightParameter.ParameterType;
      if (type1.IsByRef)
        type1 = Symbols.GetElementType(type1);
      if (type2.IsByRef)
        type2 = Symbols.GetElementType(type2);
      if (ExpandLeftParamArray && Symbols.IsParamArray(LeftParameter))
        type1 = Symbols.GetElementType(type1);
      if (ExpandRightParamArray && Symbols.IsParamArray(RightParameter))
        type2 = Symbols.GetElementType(type2);
      if (Symbols.IsNumericType(type1) && Symbols.IsNumericType(type2) && (!Symbols.IsEnum(type1) && !Symbols.IsEnum(type2)))
      {
        OverloadResolution.CompareNumericTypeSpecificity(type1, type2, ref LeftWins, ref RightWins);
      }
      else
      {
        if (LeftProcedure != null && RightProcedure != null && (Symbols.IsRawGeneric(LeftProcedure) && Symbols.IsRawGeneric(RightProcedure)))
        {
          if (type1 == type2)
            return;
          int num1 = Symbols.IndexIn(type1, LeftProcedure);
          int num2 = Symbols.IndexIn(type2, RightProcedure);
          if (num1 == num2 && num1 >= 0)
            return;
        }
        Symbols.Method OperatorMethod = (Symbols.Method) null;
        switch (ConversionResolution.ClassifyConversion(type2, type1, ref OperatorMethod))
        {
          case ConversionResolution.ConversionClass.Identity:
            break;
          case ConversionResolution.ConversionClass.Widening:
            if ((object) OperatorMethod != null && ConversionResolution.ClassifyConversion(type1, type2, ref OperatorMethod) == ConversionResolution.ConversionClass.Widening)
            {
              if (ArgumentType != null && ArgumentType == type1)
              {
                LeftWins = true;
                break;
              }
              if (ArgumentType != null && ArgumentType == type2)
              {
                RightWins = true;
                break;
              }
              BothLose = true;
              break;
            }
            LeftWins = true;
            break;
          default:
            if (ConversionResolution.ClassifyConversion(type1, type2, ref OperatorMethod) == ConversionResolution.ConversionClass.Widening)
            {
              RightWins = true;
              break;
            }
            BothLose = true;
            break;
        }
      }
    }

    private static void CompareGenericityBasedOnMethodGenericParams(ParameterInfo LeftParameter, ParameterInfo RawLeftParameter, Symbols.Method LeftMember, bool ExpandLeftParamArray, ParameterInfo RightParameter, ParameterInfo RawRightParameter, Symbols.Method RightMember, bool ExpandRightParamArray, ref bool LeftIsLessGeneric, ref bool RightIsLessGeneric, ref bool SignatureMismatch)
    {
      if (!LeftMember.IsMethod || !RightMember.IsMethod)
        return;
      Type Type1 = LeftParameter.ParameterType;
      Type Type2 = RightParameter.ParameterType;
      Type type1 = RawLeftParameter.ParameterType;
      Type type2 = RawRightParameter.ParameterType;
      if (Type1.IsByRef)
      {
        Type1 = Symbols.GetElementType(Type1);
        type1 = Symbols.GetElementType(type1);
      }
      if (Type2.IsByRef)
      {
        Type2 = Symbols.GetElementType(Type2);
        type2 = Symbols.GetElementType(type2);
      }
      if (ExpandLeftParamArray && Symbols.IsParamArray(LeftParameter))
      {
        Type1 = Symbols.GetElementType(Type1);
        type1 = Symbols.GetElementType(type1);
      }
      if (ExpandRightParamArray && Symbols.IsParamArray(RightParameter))
      {
        Type2 = Symbols.GetElementType(Type2);
        type2 = Symbols.GetElementType(type2);
      }
      if (Type1 != Type2)
      {
        SignatureMismatch = true;
      }
      else
      {
        MethodBase Method1 = LeftMember.AsMethod();
        MethodBase Method2 = RightMember.AsMethod();
        if (Symbols.IsGeneric(Method1))
          Method1 = (MethodBase) ((MethodInfo) Method1).GetGenericMethodDefinition();
        if (Symbols.IsGeneric(Method2))
          Method2 = (MethodBase) ((MethodInfo) Method2).GetGenericMethodDefinition();
        if (Symbols.RefersToGenericParameter(type1, Method1))
        {
          if (Symbols.RefersToGenericParameter(type2, Method2))
            return;
          RightIsLessGeneric = true;
        }
        else
        {
          if (!Symbols.RefersToGenericParameter(type2, Method2) || Symbols.RefersToGenericParameter(type1, Method1))
            return;
          LeftIsLessGeneric = true;
        }
      }
    }

    private static void CompareGenericityBasedOnTypeGenericParams(ParameterInfo LeftParameter, ParameterInfo RawLeftParameter, Symbols.Method LeftMember, bool ExpandLeftParamArray, ParameterInfo RightParameter, ParameterInfo RawRightParameter, Symbols.Method RightMember, bool ExpandRightParamArray, ref bool LeftIsLessGeneric, ref bool RightIsLessGeneric, ref bool SignatureMismatch)
    {
      Type Type1 = LeftParameter.ParameterType;
      Type Type2 = RightParameter.ParameterType;
      Type type1 = RawLeftParameter.ParameterType;
      Type type2 = RawRightParameter.ParameterType;
      if (Type1.IsByRef)
      {
        Type1 = Symbols.GetElementType(Type1);
        type1 = Symbols.GetElementType(type1);
      }
      if (Type2.IsByRef)
      {
        Type2 = Symbols.GetElementType(Type2);
        type2 = Symbols.GetElementType(type2);
      }
      if (ExpandLeftParamArray && Symbols.IsParamArray(LeftParameter))
      {
        Type1 = Symbols.GetElementType(Type1);
        type1 = Symbols.GetElementType(type1);
      }
      if (ExpandRightParamArray && Symbols.IsParamArray(RightParameter))
      {
        Type2 = Symbols.GetElementType(Type2);
        type2 = Symbols.GetElementType(type2);
      }
      if (Type1 != Type2)
      {
        SignatureMismatch = true;
      }
      else
      {
        Type rawDeclaringType1 = LeftMember.RawDeclaringType;
        Type rawDeclaringType2 = RightMember.RawDeclaringType;
        if (Symbols.RefersToGenericParameterCLRSemantics(type1, rawDeclaringType1))
        {
          if (Symbols.RefersToGenericParameterCLRSemantics(type2, rawDeclaringType2))
            return;
          RightIsLessGeneric = true;
        }
        else
        {
          if (!Symbols.RefersToGenericParameterCLRSemantics(type2, rawDeclaringType2))
            return;
          LeftIsLessGeneric = true;
        }
      }
    }

    private static Symbols.Method LeastGenericProcedure(Symbols.Method Left, Symbols.Method Right, OverloadResolution.ComparisonType CompareGenericity, ref bool SignatureMismatch)
    {
      bool LeftIsLessGeneric = false;
      bool RightIsLessGeneric = false;
      SignatureMismatch = false;
      if (!Left.IsMethod || !Right.IsMethod)
        return (Symbols.Method) null;
      int index = 0;
      int length1 = Left.Parameters.Length;
      int length2 = Right.Parameters.Length;
      while (index < length1 && index < length2)
      {
        switch (CompareGenericity)
        {
          case OverloadResolution.ComparisonType.GenericSpecificityBasedOnMethodGenericParams:
            OverloadResolution.CompareGenericityBasedOnMethodGenericParams(Left.Parameters[index], Left.RawParameters[index], Left, Left.ParamArrayExpanded, Right.Parameters[index], Right.RawParameters[index], Right, false, ref LeftIsLessGeneric, ref RightIsLessGeneric, ref SignatureMismatch);
            break;
          case OverloadResolution.ComparisonType.GenericSpecificityBasedOnTypeGenericParams:
            OverloadResolution.CompareGenericityBasedOnTypeGenericParams(Left.Parameters[index], Left.RawParameters[index], Left, Left.ParamArrayExpanded, Right.Parameters[index], Right.RawParameters[index], Right, false, ref LeftIsLessGeneric, ref RightIsLessGeneric, ref SignatureMismatch);
            break;
        }
        if (SignatureMismatch || LeftIsLessGeneric && RightIsLessGeneric)
          return (Symbols.Method) null;
        checked { ++index; }
      }
      if (index < length1 || index < length2)
        return (Symbols.Method) null;
      if (LeftIsLessGeneric)
        return Left;
      if (RightIsLessGeneric)
        return Right;
      return (Symbols.Method) null;
    }

    internal static Symbols.Method LeastGenericProcedure(Symbols.Method Left, Symbols.Method Right)
    {
      if (!Left.IsGeneric && !Right.IsGeneric && (!Symbols.IsGeneric(Left.DeclaringType) && !Symbols.IsGeneric(Right.DeclaringType)))
        return (Symbols.Method) null;
      bool SignatureMismatch = false;
      Symbols.Method method = OverloadResolution.LeastGenericProcedure(Left, Right, OverloadResolution.ComparisonType.GenericSpecificityBasedOnMethodGenericParams, ref SignatureMismatch);
      if ((object) method == null && !SignatureMismatch)
        method = OverloadResolution.LeastGenericProcedure(Left, Right, OverloadResolution.ComparisonType.GenericSpecificityBasedOnTypeGenericParams, ref SignatureMismatch);
      return method;
    }

    private static void InsertIfMethodAvailable(MemberInfo NewCandidate, ParameterInfo[] NewCandidateSignature, int NewCandidateParamArrayIndex, bool ExpandNewCandidateParamArray, object[] Arguments, int ArgumentCount, string[] ArgumentNames, Type[] TypeArguments, bool CollectOnlyOperators, List<Symbols.Method> Candidates)
    {
      Symbols.Method Candidate = (Symbols.Method) null;
      if (!CollectOnlyOperators)
      {
        MethodBase methodBase1 = NewCandidate as MethodBase;
        bool flag = false;
        if (NewCandidate.MemberType == MemberTypes.Method && Symbols.IsRawGeneric(methodBase1))
        {
          Candidate = new Symbols.Method(methodBase1, NewCandidateSignature, NewCandidateParamArrayIndex, ExpandNewCandidateParamArray);
          OverloadResolution.RejectUncallableProcedure(Candidate, Arguments, ArgumentNames, TypeArguments);
          NewCandidate = (MemberInfo) Candidate.AsMethod();
          NewCandidateSignature = Candidate.Parameters;
        }
        if (NewCandidate != null && NewCandidate.MemberType == MemberTypes.Method && Symbols.IsRawGeneric(NewCandidate as MethodBase))
          flag = true;
        int num1 = 0;
        int num2 = checked (Candidates.Count - 1);
        int index1 = num1;
        while (index1 <= num2)
        {
          Symbols.Method candidate = Candidates[index1];
          if ((object) candidate != null)
          {
            ParameterInfo[] parameters = candidate.Parameters;
            MethodBase methodBase2 = !candidate.IsMethod ? (MethodBase) null : candidate.AsMethod();
            if (!(NewCandidate == candidate))
            {
              int index2 = 0;
              int index3 = 0;
              int num3 = 1;
              int num4 = ArgumentCount;
              int num5 = num3;
              while (num5 <= num4)
              {
                bool BothLose = false;
                bool LeftWins = false;
                bool RightWins = false;
                OverloadResolution.CompareParameterSpecificity((Type) null, NewCandidateSignature[index2], methodBase1, ExpandNewCandidateParamArray, parameters[index3], methodBase2, candidate.ParamArrayExpanded, ref LeftWins, ref RightWins, ref BothLose);
                if (!(BothLose | LeftWins | RightWins))
                {
                  if (index2 != NewCandidateParamArrayIndex || !ExpandNewCandidateParamArray)
                    checked { ++index2; }
                  if (index3 != candidate.ParamArrayIndex || !candidate.ParamArrayExpanded)
                    checked { ++index3; }
                  checked { ++num5; }
                }
                else
                  goto label_30;
              }
              if (!OverloadResolution.IsExactSignatureMatch(NewCandidateSignature, Symbols.GetTypeParameters(NewCandidate).Length, candidate.Parameters, candidate.TypeParameters.Length))
              {
                if (!flag && (methodBase2 == null || !Symbols.IsRawGeneric(methodBase2)))
                {
                  if (!ExpandNewCandidateParamArray && candidate.ParamArrayExpanded)
                  {
                    Candidates[index1] = (Symbols.Method) null;
                  }
                  else
                  {
                    if (ExpandNewCandidateParamArray && !candidate.ParamArrayExpanded)
                      return;
                    if (ExpandNewCandidateParamArray || candidate.ParamArrayExpanded)
                    {
                      if (index2 > index3)
                        Candidates[index1] = (Symbols.Method) null;
                      else if (index3 > index2)
                        return;
                    }
                  }
                }
              }
              else if (NewCandidate.DeclaringType != candidate.DeclaringType)
              {
                if (flag || methodBase2 == null || !Symbols.IsRawGeneric(methodBase2))
                  return;
              }
              else
                break;
            }
          }
label_30:
          checked { ++index1; }
        }
      }
      if ((object) Candidate != null)
        Candidates.Add(Candidate);
      else if (NewCandidate.MemberType == MemberTypes.Property)
        Candidates.Add(new Symbols.Method((PropertyInfo) NewCandidate, NewCandidateSignature, NewCandidateParamArrayIndex, ExpandNewCandidateParamArray));
      else
        Candidates.Add(new Symbols.Method((MethodBase) NewCandidate, NewCandidateSignature, NewCandidateParamArrayIndex, ExpandNewCandidateParamArray));
    }

    internal static List<Symbols.Method> CollectOverloadCandidates(MemberInfo[] Members, object[] Arguments, int ArgumentCount, string[] ArgumentNames, Type[] TypeArguments, bool CollectOnlyOperators, Type TerminatingScope, ref int RejectedForArgumentCount, ref int RejectedForTypeArgumentCount)
    {
      int num1 = 0;
      if (TypeArguments != null)
        num1 = TypeArguments.Length;
      List<Symbols.Method> Candidates = new List<Symbols.Method>(Members.Length);
      if (Members.Length == 0)
        return Candidates;
      bool flag1 = true;
      int index1 = 0;
      do
      {
        Type declaringType = Members[index1].DeclaringType;
        if (TerminatingScope == null || !Symbols.IsOrInheritsFrom(TerminatingScope, declaringType))
        {
          do
          {
            MemberInfo member = Members[index1];
            int num2 = 0;
            ParameterInfo[] parameterInfoArray=new ParameterInfo[0];
            bool IsOtherMemberType = false;
            switch (member.MemberType)
            {
              case MemberTypes.Constructor:
              case MemberTypes.Method:
                MethodBase Method = (MethodBase) member;
                if (!CollectOnlyOperators || Symbols.IsUserDefinedOperator(Method))
                {
                  parameterInfoArray = Method.GetParameters();
                  num2 = Symbols.GetTypeParameters((MemberInfo) Method).Length;
                  if (Symbols.IsShadows(Method))
                  {
                    flag1 = false;
                    break;
                  }
                  break;
                }
                goto default;
              case MemberTypes.Event:
              case MemberTypes.Field:
              case MemberTypes.TypeInfo:
              case MemberTypes.Custom:
              case MemberTypes.NestedType:
                if (!CollectOnlyOperators)
                {
                  flag1 = false;
                  goto default;
                }
                else
                  goto default;
              case MemberTypes.Property:
                if (!CollectOnlyOperators)
                {
                  PropertyInfo propertyInfo = (PropertyInfo) member;
                  MethodInfo getMethod = propertyInfo.GetGetMethod();
                  if (getMethod != null)
                  {
                    parameterInfoArray = getMethod.GetParameters();
                    if (Symbols.IsShadows((MethodBase) getMethod))
                    {
                      flag1 = false;
                      break;
                    }
                    break;
                  }
                  MethodInfo setMethod = propertyInfo.GetSetMethod();
                  ParameterInfo[] parameters = setMethod.GetParameters();
                  parameterInfoArray = new ParameterInfo[checked (parameters.Length - 2 + 1)];
                  Array.Copy((Array) parameters, (Array) parameterInfoArray, parameterInfoArray.Length);
                  if (Symbols.IsShadows((MethodBase) setMethod))
                  {
                    flag1 = false;
                    break;
                  }
                  break;
                }
                goto default;
              default:
                IsOtherMemberType = true;
                break;
            }
            goto IsOtherMemberTypeLabel;
label_26:
            IsOtherMemberType = true;
IsOtherMemberTypeLabel:            
            if (IsOtherMemberType)
            {
              checked { ++index1; }
              continue;
            }

            int RequiredParameterCount = 0;
            int MaximumParameterCount = 0;
            int ParamArrayIndex = -1;
            Symbols.GetAllParameterCounts(parameterInfoArray, ref RequiredParameterCount, ref MaximumParameterCount, ref ParamArrayIndex);
            bool flag2 = ParamArrayIndex >= 0;
            if (ArgumentCount < RequiredParameterCount || !flag2 && ArgumentCount > MaximumParameterCount)
            {
              checked { ++RejectedForArgumentCount; }
              goto label_26;
            }
            else if (num1 > 0 && num1 != num2)
            {
              checked { ++RejectedForTypeArgumentCount; }
              goto label_26;
            }
            else
            {
              if (!flag2 || ArgumentCount == MaximumParameterCount)
                OverloadResolution.InsertIfMethodAvailable(member, parameterInfoArray, ParamArrayIndex, false, Arguments, ArgumentCount, ArgumentNames, TypeArguments, CollectOnlyOperators, Candidates);
              if (flag2)
              {
                OverloadResolution.InsertIfMethodAvailable(member, parameterInfoArray, ParamArrayIndex, true, Arguments, ArgumentCount, ArgumentNames, TypeArguments, CollectOnlyOperators, Candidates);
                goto label_26;
              }
              else
                goto label_26;
            }
          }
          while (index1 < Members.Length && Members[index1].DeclaringType == declaringType);
        }
        else
          break;
      }
      while (flag1 && index1 < Members.Length);
      int index2 = 0;
      while (index2 < Candidates.Count)
      {
        if ((object) Candidates[index2] == null)
        {
          int index3 = checked (index2 + 1);
          while (index3 < Candidates.Count && (object) Candidates[index3] == null)
            checked { ++index3; }
          Candidates.RemoveRange(index2, checked (index3 - index2));
        }
        checked { ++index2; }
      }
      return Candidates;
    }

    private static bool CanConvert(Type TargetType, Type SourceType, bool RejectNarrowingConversion, List<string> Errors, string ParameterName, bool IsByRefCopyBackContext, ref bool RequiresNarrowingConversion, ref bool AllNarrowingIsFromObject)
    {
      Type TargetType1 = TargetType;
      Type SourceType1 = SourceType;
      Symbols.Method method = (Symbols.Method) null;
      ref Symbols.Method local = ref method;
      ConversionResolution.ConversionClass conversionClass = ConversionResolution.ClassifyConversion(TargetType1, SourceType1, ref local);
      switch (conversionClass)
      {
        case ConversionResolution.ConversionClass.Identity:
        case ConversionResolution.ConversionClass.Widening:
          return true;
        case ConversionResolution.ConversionClass.Narrowing:
          if (RejectNarrowingConversion)
          {
            if (Errors != null)
            {
              OverloadResolution.ReportError(Errors,
                Interaction.IIf<string>(IsByRefCopyBackContext, "ArgumentNarrowingCopyBack3", "ArgumentNarrowing3"),
                ParameterName, SourceType, TargetType);
            }

            return false;
          }
          RequiresNarrowingConversion = true;
          if (SourceType != typeof (object))
            AllNarrowingIsFromObject = false;
          return true;
        default:
          if (Errors != null)
            OverloadResolution.ReportError(Errors, Interaction.IIf<string>(conversionClass == ConversionResolution.ConversionClass.Ambiguous, Interaction.IIf<string>(IsByRefCopyBackContext, "ArgumentMismatchAmbiguousCopyBack3", "ArgumentMismatchAmbiguous3"), Interaction.IIf<string>(IsByRefCopyBackContext, "ArgumentMismatchCopyBack3", "ArgumentMismatch3")), ParameterName, SourceType, TargetType);
          return false;
      }
    }

    private static bool InferTypeArgumentsFromArgument(Type ArgumentType, Type ParameterType, Type[] TypeInferenceArguments, MethodBase TargetProcedure, bool DigThroughToBasesAndImplements)
    {
      bool flag = OverloadResolution.InferTypeArgumentsFromArgumentDirectly(ArgumentType, ParameterType, TypeInferenceArguments, TargetProcedure, DigThroughToBasesAndImplements);
      if (flag || !DigThroughToBasesAndImplements || !Symbols.IsInstantiatedGeneric(ParameterType) || !ParameterType.IsClass && !ParameterType.IsInterface)
        return flag;
      Type genericTypeDefinition = ParameterType.GetGenericTypeDefinition();
      if (Symbols.IsArrayType(ArgumentType))
      {
        if (ArgumentType.GetArrayRank() > 1 || ParameterType.IsClass)
          return false;
        ArgumentType = typeof (IList<>).MakeGenericType(ArgumentType.GetElementType());
        if (typeof (IList<>) == genericTypeDefinition)
          goto label_25;
      }
      else if (!ArgumentType.IsClass && !ArgumentType.IsInterface || Symbols.IsInstantiatedGeneric(ArgumentType) && ArgumentType.GetGenericTypeDefinition() == genericTypeDefinition)
        return false;
      if (ParameterType.IsClass)
      {
        if (!ArgumentType.IsClass)
          return false;
        Type baseType = ArgumentType.BaseType;
        while (baseType != null && (!Symbols.IsInstantiatedGeneric(baseType) || baseType.GetGenericTypeDefinition() != genericTypeDefinition))
          baseType = baseType.BaseType;
        ArgumentType = baseType;
      }
      else
      {
        Type type = (Type) null;
        Type[] interfaces = ArgumentType.GetInterfaces();
        int index = 0;
        while (index < interfaces.Length)
        {
          Type Type = interfaces[index];
          if (Symbols.IsInstantiatedGeneric(Type) && Type.GetGenericTypeDefinition() == genericTypeDefinition)
          {
            if (type != null)
              return false;
            type = Type;
          }
          checked { ++index; }
        }
        ArgumentType = type;
      }
      if (ArgumentType == null)
        return false;
label_25:
      return OverloadResolution.InferTypeArgumentsFromArgumentDirectly(ArgumentType, ParameterType, TypeInferenceArguments, TargetProcedure, DigThroughToBasesAndImplements);
    }

    private static bool InferTypeArgumentsFromArgumentDirectly(Type ArgumentType, Type ParameterType, Type[] TypeInferenceArguments, MethodBase TargetProcedure, bool DigThroughToBasesAndImplements)
    {
      if (!Symbols.RefersToGenericParameter(ParameterType, TargetProcedure))
        return true;
      if (Symbols.IsGenericParameter(ParameterType))
      {
        if (Symbols.AreGenericMethodDefsEqual(ParameterType.DeclaringMethod, TargetProcedure))
        {
          int parameterPosition = ParameterType.GenericParameterPosition;
          if (TypeInferenceArguments[parameterPosition] == null)
            TypeInferenceArguments[parameterPosition] = ArgumentType;
          else if (TypeInferenceArguments[parameterPosition] != ArgumentType)
            return false;
        }
      }
      else
      {
        if (Symbols.IsInstantiatedGeneric(ParameterType))
        {
          Type Type1 = (Type) null;
          if (Symbols.IsInstantiatedGeneric(ArgumentType) && ArgumentType.GetGenericTypeDefinition() == ParameterType.GetGenericTypeDefinition())
            Type1 = ArgumentType;
          if (Type1 == null && DigThroughToBasesAndImplements)
          {
            Type[] interfaces = ArgumentType.GetInterfaces();
            int index = 0;
            while (index < interfaces.Length)
            {
              Type Type2 = interfaces[index];
              if (Symbols.IsInstantiatedGeneric(Type2) && Type2.GetGenericTypeDefinition() == ParameterType.GetGenericTypeDefinition())
              {
                if (Type1 != null)
                  return false;
                Type1 = Type2;
              }
              checked { ++index; }
            }
          }
          if (Type1 == null)
            return false;
          Type[] typeArguments1 = Symbols.GetTypeArguments(ParameterType);
          Type[] typeArguments2 = Symbols.GetTypeArguments(Type1);
          int num1 = 0;
          int num2 = checked (typeArguments2.Length - 1);
          int index1 = num1;
          while (index1 <= num2)
          {
            if (!OverloadResolution.InferTypeArgumentsFromArgument(typeArguments2[index1], typeArguments1[index1], TypeInferenceArguments, TargetProcedure, false))
              return false;
            checked { ++index1; }
          }
          return true;
        }
        if (Symbols.IsArrayType(ParameterType))
        {
          if (Symbols.IsArrayType(ArgumentType) && ParameterType.GetArrayRank() == ArgumentType.GetArrayRank())
            return OverloadResolution.InferTypeArgumentsFromArgument(Symbols.GetElementType(ArgumentType), Symbols.GetElementType(ParameterType), TypeInferenceArguments, TargetProcedure, DigThroughToBasesAndImplements);
          return false;
        }
      }
      return true;
    }

    private static bool CanPassToParamArray(Symbols.Method TargetProcedure, object Argument, ParameterInfo Parameter)
    {
      if (Argument == null)
        return true;
      Type parameterType = Parameter.ParameterType;
      Type argumentType = OverloadResolution.GetArgumentType(Argument);
      Type TargetType = parameterType;
      Type SourceType = argumentType;
      Symbols.Method method = (Symbols.Method) null;
      ref Symbols.Method local = ref method;
      switch (ConversionResolution.ClassifyConversion(TargetType, SourceType, ref local))
      {
        case ConversionResolution.ConversionClass.Identity:
        case ConversionResolution.ConversionClass.Widening:
          return true;
        default:
          return false;
      }
    }

    internal static bool CanPassToParameter(Symbols.Method TargetProcedure, object Argument, ParameterInfo Parameter, bool IsExpandedParamArray, bool RejectNarrowingConversions, List<string> Errors, ref bool RequiresNarrowingConversion, ref bool AllNarrowingIsFromObject)
    {
      if (Argument == null)
        return true;
      Type type = Parameter.ParameterType;
      bool isByRef = type.IsByRef;
      if (isByRef || IsExpandedParamArray)
        type = Symbols.GetElementType(type);
      Type argumentType = OverloadResolution.GetArgumentType(Argument);
      if (Argument == Missing.Value)
      {
        if (Parameter.IsOptional)
          return true;
        if (!Symbols.IsRootObjectType(type) || !IsExpandedParamArray)
        {
          if (Errors != null)
          {
            if (IsExpandedParamArray)
              OverloadResolution.ReportError(Errors, "OmittedParamArrayArgument");
            else
              OverloadResolution.ReportError(Errors, "OmittedArgument1", Parameter.Name);
          }
          return false;
        }
      }
      bool flag = OverloadResolution.CanConvert(type, argumentType, RejectNarrowingConversions, Errors, Parameter.Name, false, ref RequiresNarrowingConversion, ref AllNarrowingIsFromObject);
      if (!isByRef || !flag)
        return flag;
      return OverloadResolution.CanConvert(argumentType, type, RejectNarrowingConversions, Errors, Parameter.Name, true, ref RequiresNarrowingConversion, ref AllNarrowingIsFromObject);
    }

    internal static bool InferTypeArgumentsFromArgument(Symbols.Method TargetProcedure, object Argument, ParameterInfo Parameter, bool IsExpandedParamArray, List<string> Errors)
    {
      if (Argument == null)
        return true;
      Type type = Parameter.ParameterType;
      if (type.IsByRef || IsExpandedParamArray)
        type = Symbols.GetElementType(type);
      if (OverloadResolution.InferTypeArgumentsFromArgument(OverloadResolution.GetArgumentType(Argument), type, TargetProcedure.TypeArguments, TargetProcedure.AsMethod(), true))
        return true;
      if (Errors != null)
        OverloadResolution.ReportError(Errors, "TypeInferenceFails1", Parameter.Name);
      return false;
    }

    internal static object PassToParameter(object Argument, ParameterInfo Parameter, Type ParameterType)
    {
      bool isByRef = ParameterType.IsByRef;
      if (isByRef)
        ParameterType = ParameterType.GetElementType();
      if (Argument is Symbols.TypedNothing)
        Argument = (object) null;
      if (Argument == Missing.Value && Parameter.IsOptional)
        Argument = Parameter.DefaultValue;
      if (isByRef)
      {
        Type argumentType = OverloadResolution.GetArgumentType(Argument);
        if (argumentType != null && Symbols.IsValueType(argumentType))
          Argument = Conversions.ForceValueCopy(Argument, argumentType);
      }
      return Conversions.ChangeType(Argument, ParameterType);
    }

    private static bool FindParameterByName(ParameterInfo[] Parameters, string Name, ref int Index)
    {
      int index = 0;
      while (index < Parameters.Length)
      {
        if (Operators.CompareString(Name, Parameters[index].Name, true) == 0)
        {
          Index = index;
          return true;
        }
        checked { ++index; }
      }
      return false;
    }

    private static bool[] CreateMatchTable(int Size, int LastPositionalMatchIndex)
    {
      bool[] flagArray = new bool[checked (Size - 1 + 1)];
      int num1 = 0;
      int num2 = LastPositionalMatchIndex;
      int index = num1;
      while (index <= num2)
      {
        flagArray[index] = true;
        checked { ++index; }
      }
      return flagArray;
    }

    internal static bool CanMatchArguments(Symbols.Method TargetProcedure, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, bool RejectNarrowingConversions, List<string> Errors)
    {
      bool flag = Errors != null;
      TargetProcedure.ArgumentsValidated = true;
      if (TargetProcedure.IsMethod && Symbols.IsRawGeneric(TargetProcedure.AsMethod()))
      {
        if (TypeArguments.Length == 0)
        {
          TypeArguments = new Type[checked (TargetProcedure.TypeParameters.Length - 1 + 1)];
          TargetProcedure.TypeArguments = TypeArguments;
          if (!OverloadResolution.InferTypeArguments(TargetProcedure, Arguments, ArgumentNames, TypeArguments, Errors))
            return false;
        }
        else
          TargetProcedure.TypeArguments = TypeArguments;
        if (!OverloadResolution.InstantiateGenericMethod(TargetProcedure, TypeArguments, Errors))
          return false;
      }
      ParameterInfo[] parameters = TargetProcedure.Parameters;
      int length = ArgumentNames.Length;
      int Index = 0;
      while (length < Arguments.Length && Index != TargetProcedure.ParamArrayIndex)
      {
        if (!OverloadResolution.CanPassToParameter(TargetProcedure, Arguments[length], parameters[Index], false, RejectNarrowingConversions, Errors, ref TargetProcedure.RequiresNarrowingConversion, ref TargetProcedure.AllNarrowingIsFromObject) && !flag)
          return false;
        checked { ++length; }
        checked { ++Index; }
      }
      if (TargetProcedure.HasParamArray)
      {
        if (TargetProcedure.ParamArrayExpanded)
        {
          if (length == checked (Arguments.Length - 1) && Arguments[length] == null)
            return false;
          while (length < Arguments.Length)
          {
            if (!OverloadResolution.CanPassToParameter(TargetProcedure, Arguments[length], parameters[Index], true, RejectNarrowingConversions, Errors, ref TargetProcedure.RequiresNarrowingConversion, ref TargetProcedure.AllNarrowingIsFromObject) && !flag)
              return false;
            checked { ++length; }
          }
        }
        else
        {
          if (checked (Arguments.Length - length) != 1)
            return false;
          if (!OverloadResolution.CanPassToParamArray(TargetProcedure, Arguments[length], parameters[Index]))
          {
            if (flag)
              OverloadResolution.ReportError(Errors, "ArgumentMismatch3", parameters[Index].Name, OverloadResolution.GetArgumentType(Arguments[length]), parameters[Index].ParameterType);
            return false;
          }
        }
        checked { ++Index; }
      }
      bool[] flagArray = (bool[]) null;
      if (ArgumentNames.Length > 0 || Index < parameters.Length)
        flagArray = OverloadResolution.CreateMatchTable(parameters.Length, checked (Index - 1));
      if (ArgumentNames.Length > 0)
      {
        int[] numArray = new int[checked (ArgumentNames.Length - 1 + 1)];
        int index = 0;
        while (index < ArgumentNames.Length)
        {
          if (!OverloadResolution.FindParameterByName(parameters, ArgumentNames[index], ref Index))
          {
            if (!flag)
              return false;
            OverloadResolution.ReportError(Errors, "NamedParamNotFound2", ArgumentNames[index], TargetProcedure);
          }
          else if (Index == TargetProcedure.ParamArrayIndex)
          {
            if (!flag)
              return false;
            OverloadResolution.ReportError(Errors, "NamedParamArrayArgument1", ArgumentNames[index]);
          }
          else if (flagArray[Index])
          {
            if (!flag)
              return false;
            OverloadResolution.ReportError(Errors, "NamedArgUsedTwice2", ArgumentNames[index], TargetProcedure);
          }
          else
          {
            if (!OverloadResolution.CanPassToParameter(TargetProcedure, Arguments[index], parameters[Index], false, RejectNarrowingConversions, Errors, ref TargetProcedure.RequiresNarrowingConversion, ref TargetProcedure.AllNarrowingIsFromObject) && !flag)
              return false;
            flagArray[Index] = true;
            numArray[index] = Index;
          }
          checked { ++index; }
        }
        TargetProcedure.NamedArgumentMapping = numArray;
      }
      if (flagArray != null)
      {
        int num1 = 0;
        int num2 = checked (flagArray.Length - 1);
        int index = num1;
        while (index <= num2)
        {
          if (!flagArray[index] && !parameters[index].IsOptional)
          {
            if (!flag)
              return false;
            OverloadResolution.ReportError(Errors, "OmittedArgument1", parameters[index].Name);
          }
          checked { ++index; }
        }
      }
      return Errors == null || Errors.Count <= 0;
    }

    private static bool InstantiateGenericMethod(Symbols.Method TargetProcedure, Type[] TypeArguments, List<string> Errors)
    {
      bool flag = Errors != null;
      int num1 = 0;
      int num2 = checked (TypeArguments.Length - 1);
      int index = num1;
      while (index <= num2)
      {
        if (TypeArguments[index] == null)
        {
          if (!flag)
            return false;
          OverloadResolution.ReportError(Errors, "UnboundTypeParam1", TargetProcedure.TypeParameters[index].Name);
        }
        checked { ++index; }
      }
      if ((Errors == null || Errors.Count == 0) && !TargetProcedure.BindGenericArguments())
      {
        if (!flag)
          return false;
        OverloadResolution.ReportError(Errors, "FailedTypeArgumentBinding");
      }
      return Errors == null || Errors.Count <= 0;
    }

    internal static void MatchArguments(Symbols.Method TargetProcedure, object[] Arguments, object[] MatchedArguments)
    {
      ParameterInfo[] parameters = TargetProcedure.Parameters;
      int[] namedArgumentMapping = TargetProcedure.NamedArgumentMapping;
      int index1 = 0;
      if (namedArgumentMapping != null)
        index1 = namedArgumentMapping.Length;
      int index2 = 0;
      while (index1 < Arguments.Length && index2 != TargetProcedure.ParamArrayIndex)
      {
        MatchedArguments[index2] = OverloadResolution.PassToParameter(Arguments[index1], parameters[index2], parameters[index2].ParameterType);
        checked { ++index1; }
        checked { ++index2; }
      }
      if (TargetProcedure.HasParamArray)
      {
        if (TargetProcedure.ParamArrayExpanded)
        {
          int length = checked (Arguments.Length - index1);
          ParameterInfo Parameter = parameters[index2];
          Type elementType = Parameter.ParameterType.GetElementType();
          Array instance = Array.CreateInstance(elementType, length);
          int index3 = 0;
          while (index1 < Arguments.Length)
          {
            instance.SetValue(OverloadResolution.PassToParameter(Arguments[index1], Parameter, elementType), index3);
            checked { ++index1; }
            checked { ++index3; }
          }
          MatchedArguments[index2] = (object) instance;
        }
        else
          MatchedArguments[index2] = OverloadResolution.PassToParameter(Arguments[index1], parameters[index2], parameters[index2].ParameterType);
        checked { ++index2; }
      }
      bool[] flagArray = (bool[]) null;
      if (namedArgumentMapping != null || index2 < parameters.Length)
        flagArray = OverloadResolution.CreateMatchTable(parameters.Length, checked (index2 - 1));
      if (namedArgumentMapping != null)
      {
        int index3 = 0;
        while (index3 < namedArgumentMapping.Length)
        {
          int index4 = namedArgumentMapping[index3];
          MatchedArguments[index4] = OverloadResolution.PassToParameter(Arguments[index3], parameters[index4], parameters[index4].ParameterType);
          flagArray[index4] = true;
          checked { ++index3; }
        }
      }
      if (flagArray == null)
        return;
      int num1 = 0;
      int num2 = checked (flagArray.Length - 1);
      int index5 = num1;
      while (index5 <= num2)
      {
        if (!flagArray[index5])
          MatchedArguments[index5] = OverloadResolution.PassToParameter((object) Missing.Value, parameters[index5], parameters[index5].ParameterType);
        checked { ++index5; }
      }
    }

    private static bool InferTypeArguments(Symbols.Method TargetProcedure, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, List<string> Errors)
    {
      bool flag = Errors != null;
      ParameterInfo[] rawParameters = TargetProcedure.RawParameters;
      int length = ArgumentNames.Length;
      int Index = 0;
      while (length < Arguments.Length && Index != TargetProcedure.ParamArrayIndex)
      {
        if (!OverloadResolution.InferTypeArgumentsFromArgument(TargetProcedure, Arguments[length], rawParameters[Index], false, Errors) && !flag)
          return false;
        checked { ++length; }
        checked { ++Index; }
      }
      if (TargetProcedure.HasParamArray)
      {
        if (TargetProcedure.ParamArrayExpanded)
        {
          while (length < Arguments.Length)
          {
            if (!OverloadResolution.InferTypeArgumentsFromArgument(TargetProcedure, Arguments[length], rawParameters[Index], true, Errors) && !flag)
              return false;
            checked { ++length; }
          }
        }
        else
        {
          if (checked (Arguments.Length - length) != 1)
            return true;
          if (!OverloadResolution.InferTypeArgumentsFromArgument(TargetProcedure, Arguments[length], rawParameters[Index], false, Errors))
            return false;
        }
        checked { ++Index; }
      }
      if (ArgumentNames.Length > 0)
      {
        int index = 0;
        while (index < ArgumentNames.Length)
        {
          if (OverloadResolution.FindParameterByName(rawParameters, ArgumentNames[index], ref Index) && Index != TargetProcedure.ParamArrayIndex && (!OverloadResolution.InferTypeArgumentsFromArgument(TargetProcedure, Arguments[index], rawParameters[Index], false, Errors) && !flag))
            return false;
          checked { ++index; }
        }
      }
      return Errors == null || Errors.Count <= 0;
    }

    internal static void ReorderArgumentArray(Symbols.Method TargetProcedure, object[] ParameterResults, object[] Arguments, bool[] CopyBack, BindingFlags LookupFlags)
    {
      if (CopyBack == null)
        return;
      int num1 = 0;
      int num2 = checked (CopyBack.Length - 1);
      int index1 = num1;
      while (index1 <= num2)
      {
        CopyBack[index1] = false;
        checked { ++index1; }
      }
      if (Symbols.HasFlag(LookupFlags, BindingFlags.SetProperty) || !TargetProcedure.HasByRefParameter)
        return;
      ParameterInfo[] parameters = TargetProcedure.Parameters;
      int[] namedArgumentMapping = TargetProcedure.NamedArgumentMapping;
      int index2 = 0;
      if (namedArgumentMapping != null)
        index2 = namedArgumentMapping.Length;
      int index3 = 0;
      while (index2 < Arguments.Length && index3 != TargetProcedure.ParamArrayIndex)
      {
        if (parameters[index3].ParameterType.IsByRef)
        {
          Arguments[index2] = ParameterResults[index3];
          CopyBack[index2] = true;
        }
        checked { ++index2; }
        checked { ++index3; }
      }
      if (namedArgumentMapping == null)
        return;
      int index4 = 0;
      while (index4 < namedArgumentMapping.Length)
      {
        int index5 = namedArgumentMapping[index4];
        if (parameters[index5].ParameterType.IsByRef)
        {
          Arguments[index4] = ParameterResults[index5];
          CopyBack[index4] = true;
        }
        checked { ++index4; }
      }
    }

    private static Symbols.Method RejectUncallableProcedures(List<Symbols.Method> Candidates, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, ref int CandidateCount, ref bool SomeCandidatesAreGeneric)
    {
      Symbols.Method method = (Symbols.Method) null;
      int num1 = 0;
      int num2 = checked (Candidates.Count - 1);
      int index = num1;
      while (index <= num2)
      {
        Symbols.Method candidate = Candidates[index];
        if (!candidate.ArgumentMatchingDone)
          OverloadResolution.RejectUncallableProcedure(candidate, Arguments, ArgumentNames, TypeArguments);
        if (candidate.NotCallable)
        {
          checked { --CandidateCount; }
        }
        else
        {
          method = candidate;
          if (candidate.IsGeneric || Symbols.IsGeneric(candidate.DeclaringType))
            SomeCandidatesAreGeneric = true;
        }
        checked { ++index; }
      }
      return method;
    }

    private static void RejectUncallableProcedure(Symbols.Method Candidate, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments)
    {
      if (!OverloadResolution.CanMatchArguments(Candidate, Arguments, ArgumentNames, TypeArguments, false, (List<string>) null))
        Candidate.NotCallable = true;
      Candidate.ArgumentMatchingDone = true;
    }

    private static Type GetArgumentType(object Argument)
    {
      if (Argument == null)
        return (Type) null;
      Symbols.TypedNothing typedNothing = Argument as Symbols.TypedNothing;
      if (typedNothing != null)
        return typedNothing.Type;
      return Argument.GetType();
    }

    private static Symbols.Method MoreSpecificProcedure(Symbols.Method Left, Symbols.Method Right, object[] Arguments, string[] ArgumentNames, OverloadResolution.ComparisonType CompareGenericity, bool BothLose = false, bool ContinueWhenBothLose = false)
    {
      BothLose = false;
      bool flag1 = false;
      bool flag2 = false;
      MethodBase LeftProcedure = !Left.IsMethod ? (MethodBase) null : Left.AsMethod();
      MethodBase RightProcedure = !Right.IsMethod ? (MethodBase) null : Right.AsMethod();
      int Index1 = 0;
      int Index2 = 0;
      int length = ArgumentNames.Length;
      while (length < Arguments.Length)
      {
        Type argumentType = OverloadResolution.GetArgumentType(Arguments[length]);
        switch (CompareGenericity)
        {
          case OverloadResolution.ComparisonType.ParameterSpecificty:
            OverloadResolution.CompareParameterSpecificity(argumentType, Left.Parameters[Index1], LeftProcedure, Left.ParamArrayExpanded, Right.Parameters[Index2], RightProcedure, Right.ParamArrayExpanded, ref flag1, ref flag2, ref BothLose);
            break;
          case OverloadResolution.ComparisonType.GenericSpecificityBasedOnMethodGenericParams:
            OverloadResolution.CompareGenericityBasedOnMethodGenericParams(Left.Parameters[Index1], Left.RawParameters[Index1], Left, Left.ParamArrayExpanded, Right.Parameters[Index2], Right.RawParameters[Index2], Right, Right.ParamArrayExpanded, ref flag1, ref flag2, ref BothLose);
            break;
          case OverloadResolution.ComparisonType.GenericSpecificityBasedOnTypeGenericParams:
            OverloadResolution.CompareGenericityBasedOnTypeGenericParams(Left.Parameters[Index1], Left.RawParametersFromType[Index1], Left, Left.ParamArrayExpanded, Right.Parameters[Index2], Right.RawParametersFromType[Index2], Right, Right.ParamArrayExpanded, ref flag1, ref flag2, ref BothLose);
            break;
        }
        if (BothLose && !ContinueWhenBothLose || flag1 && flag2)
          return (Symbols.Method) null;
        if (Index1 != Left.ParamArrayIndex)
          checked { ++Index1; }
        if (Index2 != Right.ParamArrayIndex)
          checked { ++Index2; }
        checked { ++length; }
      }
      int index = 0;
      while (index < ArgumentNames.Length)
      {
        if (!OverloadResolution.FindParameterByName(Left.Parameters, ArgumentNames[index], ref Index1) || !OverloadResolution.FindParameterByName(Right.Parameters, ArgumentNames[index], ref Index2))
          throw new InternalErrorException();
        Type argumentType = OverloadResolution.GetArgumentType(Arguments[index]);
        switch (CompareGenericity)
        {
          case OverloadResolution.ComparisonType.ParameterSpecificty:
            OverloadResolution.CompareParameterSpecificity(argumentType, Left.Parameters[Index1], LeftProcedure, true, Right.Parameters[Index2], RightProcedure, true, ref flag1, ref flag2, ref BothLose);
            break;
          case OverloadResolution.ComparisonType.GenericSpecificityBasedOnMethodGenericParams:
            OverloadResolution.CompareGenericityBasedOnMethodGenericParams(Left.Parameters[Index1], Left.RawParameters[Index1], Left, true, Right.Parameters[Index2], Right.RawParameters[Index2], Right, true, ref flag1, ref flag2, ref BothLose);
            break;
          case OverloadResolution.ComparisonType.GenericSpecificityBasedOnTypeGenericParams:
            OverloadResolution.CompareGenericityBasedOnTypeGenericParams(Left.Parameters[Index1], Left.RawParameters[Index1], Left, true, Right.Parameters[Index2], Right.RawParameters[Index2], Right, true, ref flag1, ref flag2, ref BothLose);
            break;
        }
        if (BothLose && !ContinueWhenBothLose || flag1 && flag2)
          return (Symbols.Method) null;
        checked { ++index; }
      }
      if (flag1)
        return Left;
      if (flag2)
        return Right;
      return (Symbols.Method) null;
        }

    public static List<T>.Enumerator InitEnumerator<T>() //Symbols.Method
        {
            List<T> Result = new List<T>();
            return Result.GetEnumerator();
    }

    private static Symbols.Method MostSpecificProcedure(List<Symbols.Method> Candidates, ref int CandidateCount, object[] Arguments, string[] ArgumentNames)
    {
      List<Symbols.Method>.Enumerator enumerator1=InitEnumerator<Symbols.Method>();
      try
      {
        enumerator1 = Candidates.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          Symbols.Method current1 = enumerator1.Current;
          if (!current1.NotCallable && !current1.RequiresNarrowingConversion)
          {
            bool flag1 = true;
            List<Symbols.Method>.Enumerator enumerator2=InitEnumerator<Symbols.Method>();
            try
            {
              enumerator2 = Candidates.GetEnumerator();
              while (enumerator2.MoveNext())
              {
                Symbols.Method current2 = enumerator2.Current;
                if (!current2.NotCallable && !current2.RequiresNarrowingConversion && (!(current2 == current1) || current2.ParamArrayExpanded != current1.ParamArrayExpanded))
                {
                  Symbols.Method Left = current1;
                  Symbols.Method Right = current2;
                  object[] Arguments1 = Arguments;
                  string[] ArgumentNames1 = ArgumentNames;
                  int num1 = 0;
                  bool flag2 = false;
                  ref bool local = ref flag2;
                  int num2 = 1;
                  Symbols.Method method = OverloadResolution.MoreSpecificProcedure(Left, Right, Arguments1, ArgumentNames1, (OverloadResolution.ComparisonType) num1, local, num2 != 0);
                  if ((object) method == (object) current1)
                  {
                    if (!current2.LessSpecific)
                    {
                      current2.LessSpecific = true;
                      checked { --CandidateCount; }
                    }
                  }
                  else
                  {
                    flag1 = false;
                    if ((object) method == (object) current2 && !current1.LessSpecific)
                    {
                      current1.LessSpecific = true;
                      checked { --CandidateCount; }
                    }
                  }
                }
              }
            }
            finally
            {
              enumerator2.Dispose();
            }
            if (flag1)
              return current1;
          }
        }
      }
      finally
      {
        enumerator1.Dispose();
      }
      return (Symbols.Method) null;
    }

    private static Symbols.Method RemoveRedundantGenericProcedures(List<Symbols.Method> Candidates, ref int CandidateCount, object[] Arguments, string[] ArgumentNames)
    {
      int num1 = 0;
      int num2 = checked (Candidates.Count - 1);
      int index1 = num1;
      while (index1 <= num2)
      {
        Symbols.Method candidate1 = Candidates[index1];
        if (!candidate1.NotCallable)
        {
          int num3 = checked (index1 + 1);
          int num4 = checked (Candidates.Count - 1);
          int index2 = num3;
          while (index2 <= num4)
          {
            Symbols.Method candidate2 = Candidates[index2];
            if (!candidate2.NotCallable && candidate1.RequiresNarrowingConversion == candidate2.RequiresNarrowingConversion)
            {
              Symbols.Method method1 = (Symbols.Method) null;
              bool BothLose = false;
              if (candidate1.IsGeneric || candidate2.IsGeneric)
              {
                method1 = OverloadResolution.MoreSpecificProcedure(candidate1, candidate2, Arguments, ArgumentNames, OverloadResolution.ComparisonType.GenericSpecificityBasedOnMethodGenericParams, BothLose, false);
                if ((object) method1 != null)
                {
                  checked { --CandidateCount; }
                  if (CandidateCount == 1)
                    return method1;
                  if ((object) method1 == (object) candidate1)
                  {
                    candidate2.NotCallable = true;
                  }
                  else
                  {
                    candidate1.NotCallable = true;
                    break;
                  }
                }
              }
              if (!BothLose && (object) method1 == null && (Symbols.IsGeneric(candidate1.DeclaringType) || Symbols.IsGeneric(candidate2.DeclaringType)))
              {
                Symbols.Method method2 = OverloadResolution.MoreSpecificProcedure(candidate1, candidate2, Arguments, ArgumentNames, OverloadResolution.ComparisonType.GenericSpecificityBasedOnTypeGenericParams, BothLose, false);
                if ((object) method2 != null)
                {
                  checked { --CandidateCount; }
                  if (CandidateCount == 1)
                    return method2;
                  if ((object) method2 == (object) candidate1)
                  {
                    candidate2.NotCallable = true;
                  }
                  else
                  {
                    candidate1.NotCallable = true;
                    break;
                  }
                }
              }
            }
            checked { ++index2; }
          }
        }
        checked { ++index1; }
      }
      return (Symbols.Method) null;
    }

    private static void ReportError(List<string> Errors, string ResourceID, string Substitution1, Type Substitution2, Type Substitution3)
    {
      Errors.Add(Utils.GetResourceString(ResourceID, Substitution1, Utils.VBFriendlyName(Substitution2), Utils.VBFriendlyName(Substitution3)));
    }

    private static void ReportError(List<string> Errors, string ResourceID, string Substitution1, Symbols.Method Substitution2)
    {
      Errors.Add(Utils.GetResourceString(ResourceID, Substitution1, Substitution2.ToString()));
    }

    private static void ReportError(List<string> Errors, string ResourceID, string Substitution1)
    {
      Errors.Add(Utils.GetResourceString(ResourceID, new string[1]
      {
        Substitution1
      }));
    }

    private static void ReportError(List<string> Errors, string ResourceID)
    {
      Errors.Add(Utils.GetResourceString(ResourceID));
    }

    private static Exception ReportOverloadResolutionFailure(string OverloadedProcedureName, List<Symbols.Method> Candidates, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, string ErrorID, OverloadResolution.ResolutionFailure Failure, OverloadResolution.ArgumentDetector Detector, OverloadResolution.CandidateProperty CandidateFilter)
    {
      StringBuilder stringBuilder = new StringBuilder();
      List<string> Errors = new List<string>();
      int num1 = 0;
      int num2 = 0;
      int num3 = checked (Candidates.Count - 1);
      int index1 = num2;
      while (index1 <= num3)
      {
        Symbols.Method candidate = Candidates[index1];
        if (CandidateFilter(candidate))
        {
          if (candidate.HasParamArray)
          {
            int index2 = checked (index1 + 1);
            while (index2 < Candidates.Count)
            {
              if (!CandidateFilter(Candidates[index2]) || !(Candidates[index2] == candidate))
                checked { ++index2; }
              else
                goto label_12;
            }
          }
          checked { ++num1; }
          Errors.Clear();
          bool flag = Detector(candidate, Arguments, ArgumentNames, TypeArguments, Errors);
          stringBuilder.Append("\r\n    '");
          stringBuilder.Append(candidate.ToString());
          stringBuilder.Append("':");
          List<string>.Enumerator enumerator=InitEnumerator<string>();
          try
          {
            enumerator = Errors.GetEnumerator();
            while (enumerator.MoveNext())
            {
              string current = enumerator.Current;
              stringBuilder.Append("\r\n        ");
              stringBuilder.Append(current);
            }
          }
          finally
          {
            enumerator.Dispose();
          }
        }
label_12:
        checked { ++index1; }
      }
      string resourceString = Utils.GetResourceString(ErrorID, OverloadedProcedureName, stringBuilder.ToString());
      if (num1 == 1)
        return (Exception) new InvalidCastException(resourceString);
      return (Exception) new AmbiguousMatchException(resourceString);
    }

    private static bool DetectArgumentErrors(Symbols.Method TargetProcedure, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, List<string> Errors)
    {
      return OverloadResolution.CanMatchArguments(TargetProcedure, Arguments, ArgumentNames, TypeArguments, false, Errors);
    }

    private static bool CandidateIsNotCallable(Symbols.Method Candidate)
    {
      return Candidate.NotCallable;
    }

    private static Exception ReportUncallableProcedures(string OverloadedProcedureName, List<Symbols.Method> Candidates, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, OverloadResolution.ResolutionFailure Failure)
    {
      return OverloadResolution.ReportOverloadResolutionFailure(OverloadedProcedureName, Candidates, Arguments, ArgumentNames, TypeArguments, "NoCallableOverloadCandidates2", Failure, new OverloadResolution.ArgumentDetector(OverloadResolution.DetectArgumentErrors), new OverloadResolution.CandidateProperty(OverloadResolution.CandidateIsNotCallable));
    }

    private static bool DetectArgumentNarrowing(Symbols.Method TargetProcedure, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, List<string> Errors)
    {
      return OverloadResolution.CanMatchArguments(TargetProcedure, Arguments, ArgumentNames, TypeArguments, true, Errors);
    }

    private static bool CandidateIsNarrowing(Symbols.Method Candidate)
    {
      return !Candidate.NotCallable && Candidate.RequiresNarrowingConversion;
    }

    private static Exception ReportNarrowingProcedures(string OverloadedProcedureName, List<Symbols.Method> Candidates, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, OverloadResolution.ResolutionFailure Failure)
    {
      return OverloadResolution.ReportOverloadResolutionFailure(OverloadedProcedureName, Candidates, Arguments, ArgumentNames, TypeArguments, "NoNonNarrowingOverloadCandidates2", Failure, new OverloadResolution.ArgumentDetector(OverloadResolution.DetectArgumentNarrowing), new OverloadResolution.CandidateProperty(OverloadResolution.CandidateIsNarrowing));
    }

    private static bool DetectUnspecificity(Symbols.Method TargetProcedure, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, List<string> Errors)
    {
      OverloadResolution.ReportError(Errors, "NotMostSpecificOverload");
      return false;
    }

    private static bool CandidateIsUnspecific(Symbols.Method Candidate)
    {
      return !Candidate.NotCallable && !Candidate.RequiresNarrowingConversion && !Candidate.LessSpecific;
    }

    private static Exception ReportUnspecificProcedures(string OverloadedProcedureName, List<Symbols.Method> Candidates, OverloadResolution.ResolutionFailure Failure)
    {
      return OverloadResolution.ReportOverloadResolutionFailure(OverloadedProcedureName, Candidates, (object[]) null, (string[]) null, (Type[]) null, "NoMostSpecificOverload2", Failure, new OverloadResolution.ArgumentDetector(OverloadResolution.DetectUnspecificity), new OverloadResolution.CandidateProperty(OverloadResolution.CandidateIsUnspecific));
    }

    internal static Symbols.Method ResolveOverloadedCall(string MethodName, List<Symbols.Method> Candidates, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, BindingFlags LookupFlags, bool ReportErrors, ref OverloadResolution.ResolutionFailure Failure)
    {
      Failure = OverloadResolution.ResolutionFailure.None;
      int count = Candidates.Count;
      bool SomeCandidatesAreGeneric = false;
      Symbols.Method method1 = OverloadResolution.RejectUncallableProcedures(Candidates, Arguments, ArgumentNames, TypeArguments, ref count, ref SomeCandidatesAreGeneric);
      switch (count)
      {
        case 0:
          Failure = OverloadResolution.ResolutionFailure.InvalidArgument;
          if (ReportErrors)
            throw OverloadResolution.ReportUncallableProcedures(MethodName, Candidates, Arguments, ArgumentNames, TypeArguments, Failure);
          return (Symbols.Method) null;
        case 1:
          return method1;
        default:
          if (SomeCandidatesAreGeneric)
          {
            method1 = OverloadResolution.RemoveRedundantGenericProcedures(Candidates, ref count, Arguments, ArgumentNames);
            if (count == 1)
              return method1;
          }
          int num = 0;
          Symbols.Method method2 = (Symbols.Method) null;
          List<Symbols.Method>.Enumerator enumerator=InitEnumerator<Symbols.Method>();
          try
          {
            enumerator = Candidates.GetEnumerator();
            while (enumerator.MoveNext())
            {
              Symbols.Method current = enumerator.Current;
              if (!current.NotCallable)
              {
                if (current.RequiresNarrowingConversion)
                {
                  checked { --count; }
                  if (current.AllNarrowingIsFromObject)
                  {
                    checked { ++num; }
                    method2 = current;
                  }
                }
                else
                  method1 = current;
              }
            }
          }
          finally
          {
            enumerator.Dispose();
          }
          switch (count)
          {
            case 0:
              if (num == 1)
                return method2;
              Failure = OverloadResolution.ResolutionFailure.AmbiguousMatch;
              if (ReportErrors)
                throw OverloadResolution.ReportNarrowingProcedures(MethodName, Candidates, Arguments, ArgumentNames, TypeArguments, Failure);
              return (Symbols.Method) null;
            case 1:
              return method1;
            default:
              Symbols.Method method3 = OverloadResolution.MostSpecificProcedure(Candidates, ref count, Arguments, ArgumentNames);
              if ((object) method3 != null)
                return method3;
              Failure = OverloadResolution.ResolutionFailure.AmbiguousMatch;
              if (ReportErrors)
                throw OverloadResolution.ReportUnspecificProcedures(MethodName, Candidates, Failure);
              return (Symbols.Method) null;
          }
      }
    }

    internal static Symbols.Method ResolveOverloadedCall(string MethodName, MemberInfo[] Members, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, BindingFlags LookupFlags, bool ReportErrors, ref OverloadResolution.ResolutionFailure Failure)
    {
      int RejectedForArgumentCount = 0;
      int RejectedForTypeArgumentCount = 0;
      List<Symbols.Method> Candidates = OverloadResolution.CollectOverloadCandidates(Members, Arguments, Arguments.Length, ArgumentNames, TypeArguments, false, (Type) null, ref RejectedForArgumentCount, ref RejectedForTypeArgumentCount);
      if (Candidates.Count == 1 && !Candidates[0].NotCallable)
        return Candidates[0];
      if (Candidates.Count != 0)
        return OverloadResolution.ResolveOverloadedCall(MethodName, Candidates, Arguments, ArgumentNames, TypeArguments, LookupFlags, ReportErrors, ref Failure);
      Failure = OverloadResolution.ResolutionFailure.MissingMember;
      if (ReportErrors)
      {
        string ResourceKey = "NoViableOverloadCandidates1";
        if (RejectedForArgumentCount > 0)
          ResourceKey = "NoArgumentCountOverloadCandidates1";
        else if (RejectedForTypeArgumentCount > 0)
          ResourceKey = "NoTypeArgumentCountOverloadCandidates1";
        throw new MissingMemberException(Utils.GetResourceString(ResourceKey, new string[1]
        {
          MethodName
        }));
      }
      return (Symbols.Method) null;
    }

    internal enum ResolutionFailure
    {
      None,
      MissingMember,
      InvalidArgument,
      AmbiguousMatch,
      InvalidTarget,
    }

    private enum ComparisonType
    {
      ParameterSpecificty,
      GenericSpecificityBasedOnMethodGenericParams,
      GenericSpecificityBasedOnTypeGenericParams,
    }

    private delegate bool ArgumentDetector(Symbols.Method TargetProcedure, object[] Arguments, string[] ArgumentNames, Type[] TypeArguments, List<string> Errors);

    private delegate bool CandidateProperty(Symbols.Method Candidate);
  }
}
