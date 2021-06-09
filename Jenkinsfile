@Library('freebyTech')_

import com.freebyTech.BuildInfo

String versionPrefix = '2.1'
String repository = 'trackitsvalue'    
String imageName = 'tiv-api-budget'
String dockerBuildArguments = ''
BuildInfo buildInfo

node 
{
  buildInfo = build(this, versionPrefix, repository, imageName, dockerBuildArguments, true, true)

  if('main'.equalsIgnoreCase(env.BRANCH_NAME)) 
  {
    env.NAMESPACE = 'production'
    deploy(buildInfo, repository, imageName, 'istio-system/api-trackitsvalue-production-gateway', 'api.trackitsvalue.com')
  } 
  else if('develop'.equalsIgnoreCase(env.BRANCH_NAME)) 
  {
    env.NAMESPACE = 'beta'
    deploy(buildInfo, repository, imageName, 'istio-system/beta-api-trackitsvalue-development-gateway', 'beta-api.trackitsvalue.com')
  }
}

